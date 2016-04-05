import React from 'react'

var Grid = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  render() {

    var self = this;
    var collection = this.props.collection;

    var collection = collection.map(function(item, index) {
      return (
        <self.props.tile
          key={index}
          content={item}
          onRemove={self.props.onRemove}
          navigate={self.props.navigate}
          url={self.props.url}
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

    var collection = this.props.collection;

    return (
      <div className="page-header">
        <a href="#" onClick={this.showDialog}>Change {this.props.name}...</a>

        <this.props.selector
          initialValue={collection}
          url={this.props.url}
          onChange={this.props.onChange}
          ref="dialog"
        />

      </div>
    );
  }

});

export default Grid;
