var SingleRoleView = React.createClass({

  getInitialState() {
    return {
      role: null
    };
  },

  componentDidMount() {
    this.getRole();
  },

  getRole() {

    var url = "/api/roles/" + this.props.id;

    $.ajax({
      url: url,
      cache: false,
      success: function(data) {
        this.setState({
          role: data
        });
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(url, status, err.toString());
      }
    });

  },

  onPermissionRemoved(permission) {

    var role = this.state.role;
    var newCollection = role.permissions.filter(function(perm) {
      return perm.id != permission.id;
    });

    role.permissions = newCollection;

    this.setState({
      role: role
    });

  },

  render() {

    var role = this.state.role;
    var self = this;

    if (role == null)
      return (<h1>Unknown role {this.props.id}</h1>);

    var permissions = role.permissions.map(function(permission, index) {
      return (
        <li key={index} className="col-sm-3">
          <PermissionPill permission={permission}>
            <RemovePermission
              permission={permission}
              onRemove={self.onPermissionRemoved}
              url={"/api/roles/" + role.key + "/permission/" + permission.key}
              action="Remove"
              from={role.name}
            />
          </PermissionPill>
        </li>
      );
    });

    return (
      <div className="well">
        <h1>{role.name}<small className="pull-right">{role.key}</small></h1>

        <div className="page-header">
          <h4>Permissions</h4>
        </div>
        <ul className="nav nav-pills">
          {permissions}
        </ul>

      </div>
    )
  }
});
