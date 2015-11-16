var UserTile = React.createClass({

  getInitialState() {
    return {
      checked: false
    };
  },

  onClick() {
    var newState = !this.state.checked;

    this.setState({
      checked: newState
    });

    this.props.onChange(newState);
  },

  navigateToDetails(e) {
    e.preventDefault();
    this.props.navigate("singleuser", { key: this.props.user.key});
  },

  deleteUser(e) {
    e.preventDefault();
    this.refs.deleteDialog.open();
  },

  onSubmit() {
    var dialog = this.refs.deleteDialog;
    var user = this.props.user;

    dialog.asyncStart();

    $.ajax({
      url: "/api/users/" + user.key,
      method: 'DELETE',
      cache: false,
      success: function(data) {
        dialog.asyncStop();
        dialog.close();
        this.props.onUserRemoved(user);
      }.bind(this),
      error: function(xhr, status, err) {
        dialog.asyncStop();
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });
  },

  render() {
    var user = this.props.user;
    var checked = this.state.checked;

    var styleName = checked ? "panel panel-info" : "panel panel-default";

    return (
      <div className="col-md-3" onClick={this.onClick}>
        <div className={styleName}>
          <div className="panel-heading">
            <h3 className="panel-title">
              <a onClick={this.navigateToDetails} href="#">
                {user.name}
              </a>
              <a className="pull-right danger" onClick={this.deleteUser} href="#">
                <span className="glyphicon glyphicon-remove-circle"></span>
              </a>
            </h3>
            <Dialog title="Confirm User Delete" acceptText="Delete" acceptStyle="danger" onSubmit={this.onSubmit} size="medium" ref="deleteDialog">
              <p>Are you sure you want to delete the user <strong>{user.name}</strong>?</p>
            </Dialog>
          </div>
          <div className="panel-body">
            <div>Roles: {user.roles.length}</div>
            <div>Includes: {user.includes.length}</div>
            <div>Revokes: {user.revokes.length}</div>
          </div>
        </div>
      </div>
    );
  }
})
