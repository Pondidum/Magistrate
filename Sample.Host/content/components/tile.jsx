var Tile = React.createClass({

  statics: {
    sizes: {
      small: "sm",
      large: "lg"
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

    var deleteControl;

    if (this.props.onDelete) {
      deleteControl = (
        <li>
          <a href="#" onClick={this.openDeleteDialog}>
            <span className="glyphicon glyphicon-remove-circle"></span>
          </a>
        </li>
      );
    }

    var body;

    if (this.props.tileSize == Tile.sizes.large) {
      body = (
        <div className="panel-body" style={{ height: "100px" }}>
          {this.props.children}
        </div>
      );
    }

    return (
      <li className="col-md-3">
        <div className="panel panel-default">
          <div className="panel-heading">
            <h3 className="panel-title">
              {title}
              <ul className="tile-actions pull-right list-unstyled list-inline">
                {deleteControl}
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

  }
});
