var RemovePermission = React.createClass({

  propTypes: {
    permission: React.PropTypes.object.isRequired,
    onRemove: React.PropTypes.func.isRequired,
    url: React.PropTypes.string.isRequired,
    action: React.PropTypes.string.isRequired,
    from: React.PropTypes.string.isRequired,
  },

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  onSubmit() {

    var dialog = this.refs.dialog;
    dialog.asyncStart();

    var permission = this.props.permission;
    var onRemove = this.props.onRemove;

    $.ajax({
      url: this.props.url,
      method: "DELETE",
      cache: false,
      success: function(data) {
        dialog.asyncStop();
        dialog.close();
        onRemove(permission);
      }.bind(this),
      error: function(xhr, status, err) {
        dialog.asyncStop();
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });

  },

  render() {

    var permission = this.props.permission;
    var action = this.props.action;
    var from = this.props.from;

    return (
      <a className="permission-delete" onClick={this.showDialog} href="#">
        <span className="glyphicon glyphicon-remove"></span>

        <Dialog title={action + " Permission"} onSubmit={this.onSubmit} acceptText={action} acceptStyle="danger" ref="dialog">
          <p>Are you sure you wish to {action.toLowerCase()} the permission <strong>{permission.name}</strong> from <strong>{from}</strong>?</p>
        </Dialog>
      </a>
    );
  }
});
