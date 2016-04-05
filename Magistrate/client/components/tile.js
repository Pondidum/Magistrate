import React from 'react'
import { connect } from 'react-redux'

import Dialog from './dialog'

const mapStateToProps = (state, ownProps) => {
  return {
    tileSize: state.ui.tileSize,
    ...ownProps
  }
}

var Tile = React.createClass({

  statics: {
    sizes: {
      small: "sm",
      large: "lg",
      table: "tbl"
    }
  },

  openDeleteDialog(e) {
      e.preventDefault();
      this.refs.deleteDialog.open();
  },

  handleDeleteSubmit() {

    var dialog = this.refs.deleteDialog;
    dialog.asyncStart();

    this.props.onDelete(
      function() {
        dialog.asyncStop();
        dialog.close();
      },
      function() {
        dialog.asyncStop();
      });

  },

  render() {

    var title;

    if (this.props.navigateTo)
      title = (<a onClick={this.props.navigateTo} href="#">{this.props.title}</a>);
    else
      title = (<span>{this.props.title}</span>);

    if (this.props.tileSize == Tile.sizes.table) {
      return this.renderRow(title);
    }

    var body;

    if (this.props.tileSize == Tile.sizes.large) {
      body = (
        <div className="panel-body" style={{ height: "100px" }}>
          {this.renderChildren()}
        </div>
      );
    }

    return (
      <li className="col-md-3 tile">
        <div className="panel panel-default">
          <div className="panel-heading">
            <h3 className="panel-title">
              {title}
              <ul className="tile-actions pull-right list-unstyled list-inline">
                <li>
                  <a href="#" onClick={this.openDeleteDialog}>
                    <span className="glyphicon glyphicon-remove-circle"></span>
                  </a>
                </li>
              </ul>
            </h3>
            <Dialog title="Confirm Delete" acceptText="Delete" acceptStyle="danger" onSubmit={this.handleDeleteSubmit} size="lg" ref="deleteDialog">
              {this.props.dialogContent}
            </Dialog>
          </div>
          {body}
        </div>
      </li>
    );

  },

  renderChildren() {
    var children = this.props.children;

    if (children.constructor !== Array) {
      return (<span>{children}</span>);
    }

    var list = children.map(function(child, index) {
      return (<li key={index}>{child}</li>);
    });

    return (
      <ul className={"list-unstyled " + (this.props.tileSize == Tile.sizes.table ? "list-inline" : "")}>
        {list}
      </ul>
    );
  },

  renderRow(title) {

    return (
      <li className="col-md-12 tile">
        <div className="row panel panel-default">
          <div className="panel-body">
            <div className="col-sm-3">{title}</div>
            <div className="col-sm-8">{this.renderChildren()}</div>
            <div className="col-sm-1">
              <a href="#" onClick={this.openDeleteDialog}>
                <span className="glyphicon glyphicon-remove-circle"></span>
              </a>
            </div>
          </div>
        </div>
      </li>
    );
  },

});

export default connect(mapStateToProps)(Tile)
