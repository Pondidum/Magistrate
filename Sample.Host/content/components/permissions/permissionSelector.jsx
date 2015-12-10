var Input = ReactBootstrap.Input;

var PermissionSelector = React.createClass({

  getInitialState() {
    return {
      permissions: [],
      selection: this.getInitialSelection()
    };
  },

  componentDidMount() {
    this.getPermissions();
  },

  getInitialSelection() {
    var selection = {};
    this.props.initialValue.forEach(function(permission) {
      selection[permission.key] = true;
    });

    return selection;
  },

  open() {
    this.setState({ selection: this.getInitialSelection() });
    this.refs.dialog.open();
  },

  getPermissions() {

    $.ajax({
      url: "/api/permissions/all",
      cache: false,
      success: function(permissions) {
        this.setState({ permissions: permissions });
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

      var all = self.state.permissions;
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

    var permissions = this.state.permissions.map(function(permission, index) {

      var onChange = function(e) {
        var selection = self.state.selection;
        selection[permission.key] = e.target.checked;

        self.setState({ selection: selection });
      }

      var isSelected = self.state.selection[permission.key];

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
          <td>{permission.name}</td>
          <td>{permission.description}</td>
        </tr>
      );
    });

    return (
      <Dialog title="Select Permissions" onSubmit={this.onSubmit} acceptText="Update" ref="dialog">
        <table className="table">
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            {permissions}
          </tbody>
        </table>
      </Dialog>
    );
  }

});
