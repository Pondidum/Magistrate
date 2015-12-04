var Tile = React.createClass({

  openDeleteDialog(e) {
      e.preventDefault();
      this.refs.deleteDialog.open();
  },

  onDelete() {
    var dialog = this.refs.deleteDialog;

    dialog.asyncStart();
    $.ajax({
      url: this.props.deleteUrl,
      method: 'DELETE',
      cache: false,
      success: function(data) {
        dialog.asyncStop();
        dialog.close();
        this.props.onDelete();
      }.bind(this),
      error: function(xhr, status, err) {
        dialog.asyncStop();
        console.error(this.props.deleteUrl, status, err.toString());
      }.bind(this)
    });
  },

  render() {

    var deleteControl;

    if (this.props.deleteUrl) {
      deleteControl = (
        <li>
          <a href="#" onClick={this.openDeleteDialog}>
            <span className="glyphicon glyphicon-remove-circle"></span>
          </a>
        </li>
      );
    }

    var editControl;

    if (this.props.editAction) {
      editControl = (
        <li>
          <a href="#" onClick={this.props.editAction}>
            <span className="glyphicon glyphicon-pencil"></span>
          </a>
        </li>
      );
    }

    return (
      <div className="panel panel-default">
        <div className="panel-heading">
          <h3 className="panel-title">
            <a onClick={this.props.navigateTo} href="#">
              {this.props.title}
            </a>
            <ul className="tile-actions pull-right list-unstyled list-inline">
              {editControl}
              {deleteControl}
            </ul>
          </h3>
          <Dialog title="Confirm Delete" acceptText="Delete" acceptStyle="danger" onSubmit={this.onDelete} size="medium" ref="deleteDialog">
            {this.props.dialogContent}
          </Dialog>
        </div>
        <div className="panel-body" style={{ height: "100px" }}>
          {this.props.children}
        </div>
      </div>
    );

  }
});
