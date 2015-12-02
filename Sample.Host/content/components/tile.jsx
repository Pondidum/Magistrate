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

    return (
      <div className="panel panel-default">
        <div className="panel-heading">
          <h3 className="panel-title">
            <a onClick={this.props.navigateTo} href="#">
              {this.props.title}
            </a>
            <a className="pull-right danger" onClick={this.openDeleteDialog} href="#">
              <span className="glyphicon glyphicon-remove-circle"></span>
            </a>
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
