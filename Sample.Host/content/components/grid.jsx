var Grid = React.createClass({

  getInitialState() {
    return {
      collection: this.props.collection
    };
  },

  onRemove(item) {

    var newCollection = this.state.collection.filter(function(p) {
      return p.key != item.key;
    });

    this.setState({ collection: newCollection });
  },

  onCollectionChanged(added, removed) {

    var current = this.state.collection;

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

    var collection = this.state.collection.map(function(item, index) {
      return (
        <li key={index} className="col-md-3">
          <self.props.tile
            content={item}
            onRemove={self.onRemove}
            navigate={self.props.navigate}
            url={self.props.url}
            tileSize={self.props.tileSize}
          />
        </li>
      );
    });

    return (
      <div>

        <div className="page-header">
          <a href="#" onClick={this.showDialog}>Change {this.props.name}...</a>

          <this.props.selector
            initialValue={this.state.collection}
            url={self.props.url}
            onChange={this.onCollectionChanged}
            ref="dialog"
          />

        </div>

        <ul className="list-unstyled list-inline row">
          {collection}
        </ul>

      </div>
    );

  }

});
