var PermissionPill = React.createClass({

  render() {

    var permission = this.props.permission;
    var user = this.props.user;
    var onPermissionRemoved = this.props.onPermissionRemoved;

    return (
      <div className="permission-pill">
        <span>{permission.name}</span>
        <RemovePermission permission={permission} user={user} onPermissionRemoved={onPermissionRemoved} />
      </div>
    );
  }

});


var RemovePermission = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  onSubmit() {

    var dialog = this.refs.dialog;
    dialog.asyncStart();

    var permission = this.props.permission;
    var user = this.props.user;
    var url = "/api/users/" + user.key + "/removePermission/" + permission.key;
    var onPermissionRemoved = this.props.onPermissionRemoved || function() {};

    $.ajax({
      url: url,
      method: "PUT",
      cache: false,
      success: function(data) {
        dialog.asyncStop();
        dialog.close();
        onPermissionRemoved(permission);
      }.bind(this),
      error: function(xhr, status, err) {
        dialog.asyncStop();
        console.error(url, status, err.toString());
      }.bind(this)
    });

  },

  render() {

    var permission = this.props.permission;
    var user = this.props.user;

    return (
      <a className="pull-right permission-delete" onClick={this.showDialog} href="#">
        <span className="glyphicon glyphicon-remove"></span>

        <Dialog title="Remove Permission" onSubmit={this.onSubmit} acceptText="Remove" acceptStyle="danger" ref="dialog">
          <p>Are you sure you wish to delete <strong>{permission.name}</strong> from the <strong>{user.name}</strong>?</p>
        </Dialog>
      </a>
    );
  }
});
