var Overview = React.createClass({

  getInitialState() {
    return {
      filter: "",
      collection: [],
    };
  },

  componentDidMount() {
    this.loadCollection();
  },

  loadCollection() {

    $.ajax({
      url: this.props.url + "/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          collection: data || []
        });
      }.bind(this)
    });

  },

  onFilterChanged(value) {
    this.setState({
      filter: value
    });
  },

  onAdd(item) {
    var newCollection = this.state.collection.concat([item]);

    this.setState({
      collection: newCollection
    });
  },

  onRemove(item) {

    var newCollection = this.state.collection.filter(function(x) {
      return x.key !== item.key
    });

    this.setState({
      collection: newCollection
    });

  },

  render() {

    var self = this;
    var filter = new RegExp(this.state.filter, "i");

    var collection = this.state.collection
      .filter(function(item) {
        return item.name.search(filter) != -1;
      })
      .map(function(item, index) {
        return (
          <self.props.tile
            key={index}
            content={item}
            onRemove={self.onRemove}
            navigate={self.props.navigate}
            url={self.props.url}
            tileSize={self.props.tileSize}
          />
        );
      });

    return (
      <div>
        <div>
          <div className="row">
            <div className="col-sm-2">
              <self.props.create onCreate={this.onAdd} url={self.props.url} />
            </div>
            <FilterBar filterChanged={this.onFilterChanged} />
          </div>
          <ul className="list-unstyled list-inline">
            {collection}
          </ul>
        </div>
      </div>
    );

  }

});
