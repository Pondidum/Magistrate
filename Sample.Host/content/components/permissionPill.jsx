var PermissionPill = React.createClass({

  render() {

    var permission = this.props.permission;

    return (
      <div className="permission-pill">
        <span>{permission.name}</span>
        <div className="pull-right">
          {this.props.children}
        </div>
      </div>
    );
  }

});
