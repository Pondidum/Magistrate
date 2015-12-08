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

  onNameChanged(newName) {

    var url = "/api/roles/" + this.props.id + "/name";
    var json = JSON.stringify({
      name: newName
    });

    $.ajax({
      url: url,
      cache: false,
      method: "PUT",
      data: json,
      success: function() {

        var role = this.state.role;
        role.name = newName;

        this.setState({ role: role });

      }.bind(this),
      error: function(xhr, status, err) {
        console.error(url, status, err.toString());
      }
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

        <h1><InlineEditor initialValue={role.name} onChange={this.onNameChanged} /></h1>
        <div>{role.description}</div>

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
