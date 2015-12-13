var Input = ReactBootstrap.Input;

var SelectorDialog = React.createClass({

  getInitialState() {
    return {
      collection: [],
      selection: this.getInitialSelection()
    };
  },

  componentDidMount() {
    this.getCollection();
  },

  getInitialSelection() {
    var selection = {};
    this.props.initialValue.forEach(function(item) {
      selection[item.key] = true;
    });

    return selection;
  },

  open() {
    this.setState({ selection: this.getInitialSelection() });
    this.refs.dialog.open();
  },

  getCollection() {

    $.ajax({
      url: this.props.collectionUrl,
      cache: false,
      success: function(collection) {
        this.setState({ collection: collection });
      }.bind(this)
    });

  },

  onSubmit() {
    var self = this;
    var dialog = this.refs.dialog;
    dialog.asyncStart();

    var initial = this.getInitialSelection();
    var selection = this.state.selection;

    var toAdd = [];
    var toRemove = [];

    for(key in selection) {

      if (selection[key] != initial[key]) {

        if (selection[key] == true)
          toAdd.push(key);

        if (selection[key] == false && initial[key] == true)
          toRemove.push(key);
      }
    }

    var actions = [];
    var url = this.props.url;

    if (toAdd.length > 0) {
      actions.push(function(callback) {
         $.ajax({
           url: url,
           cache: false,
           method: "PUT",
           data: JSON.stringify(toAdd),
           success: function() {
             callback();
           }
         });
      });
    }

    if (toRemove.length > 0) {
      actions.push(function(callback) {
         $.ajax({
           url: url,
           cache: false,
           method: "DELETE",
           data: JSON.stringify(toRemove),
           success: function() {
             callback();
           }
         });
      });
    }

    async.series(actions, function(err, results) {

      var all = self.state.collection;
      var added = toAdd.map(function(key) {
        return all.find(p => p.key == key);
      });

      var removed = toRemove.map(function(key) {
        return all.find(p => p.key == key);
      });

      self.props.onChange(added, removed);

      dialog.asyncStop();
      dialog.close();
    });

  },

  render() {

    var self = this;

    var collection = this.state.collection.map(function(item, index) {

      var onChange = function(e) {
        var selection = self.state.selection;
        selection[item.key] = e.target.checked;

        self.setState({ selection: selection });
      }

      var isSelected = self.state.selection[item.key];

      return (
        <tr key={index}>
          <td>
            <div className="checkbox">
              <label>
                <input type="checkbox" onChange={onChange} checked={isSelected}/>
                <span className="checkbox-material">
                  <span className="check"></span>
                </span>
              </label>
            </div>
          </td>
          <td>{item.name}</td>
          <td>{item.description}</td>
        </tr>
      );
    });

    return (
      <Dialog title={"Select " + this.props.name} onSubmit={this.onSubmit} acceptText="Update" ref="dialog">
        <table className="table">
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            {collection}
          </tbody>
        </table>
      </Dialog>
    );
  }

});
