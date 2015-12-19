var Grid = React.createClass({

  getInitialState() {
    return {
      collection: null
    };
  },

  getCollection() {
    return this.state.collection || this.props.collection;
  },

  onRemove(item) {

    var newCollection = this.getCollection().filter(function(p) {
      return p.key != item.key;
    });

    this.setState({ collection: newCollection });
  },

  onCollectionChanged(added, removed) {

    var current = this.getCollection();

    current = current.concat(added);
    current = current.filter(function(item) {
      return removed.find(p => p.key == item.key) == null;
    });

    this.setState({ collection: current });
  },

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  render() {

    var self = this;
    var collection = this.getCollection();

    var collection = collection.map(function(item, index) {
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

        {this.renderHeader()}

        <div className="row">
          <ul className="list-unstyled list-inline col-sm-12">
            {collection}
          </ul>
        </div>

      </div>
    );

  },

  renderHeader() {

    if (this.props.selector == null)
      return null;

    var collection = this.getCollection();

    return (
      <div className="page-header">
        <a href="#" onClick={this.showDialog}>Change {this.props.name}...</a>

        <this.props.selector
          initialValue={collection}
          url={this.props.url}
          onChange={this.onCollectionChanged}
          ref="dialog"
        />

      </div>
    );
  }

});
