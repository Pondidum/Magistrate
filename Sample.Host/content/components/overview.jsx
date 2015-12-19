var Overview = React.createClass({

  getInitialState() {
    return {
      filter: "",
      collection: null,
    };
  },

  getCollection() {
    return this.state.collection || this.props.collection;
  },

  onFilterChanged(value) {
    this.setState({
      filter: value
    });
  },

  onAdd(item) {
    var newCollection = this.getCollection().concat([item]);

    this.setState({
      collection: newCollection
    });
  },

  onRemove(item) {

    var newCollection = this.getCollection().filter(function(x) {
      return x.key !== item.key
    });

    this.setState({
      collection: newCollection
    });

  },

  render() {

    var filter = new RegExp(this.state.filter, "i");

    var collection = this
      .getCollection()
      .filter(function(item) {
        var isName =  item.name.search(filter) != -1;
        var isDescription = (item.description || "").search(filter) != -1;

        return isName || isDescription;
      });

    return (
      <div>
        <div>
          <div className="row">
            <div className="col-sm-2">
              <this.props.create onCreate={this.onAdd} url={this.props.url} />
            </div>
            <FilterBar filterChanged={this.onFilterChanged} />
          </div>
            <Grid
              name="???"
              tile={this.props.tile}
              {...this.props}
              collection={collection}
            />

        </div>
      </div>
    );

  }

});
