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
      }.bind(this)
    });

  },

  onPermissionsChanged() {
    this.getRole();
  },

  onPermissionRemoved(permission) {

    var role = this.state.role;
    var newCollection = role.permissions.filter(function(perm) {
      return perm.key != permission.key;
    });

    role.permissions = newCollection;

    this.setState({
      role: role
    });

  },

  onNameChanged(newName) {

    var json = JSON.stringify({
      name: newName
    });

    $.ajax({
      url: "/api/roles/" + this.props.id + "/name",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {

        var role = this.state.role;
        role.name = newName;

        this.setState({ role: role });

      }.bind(this)
    });

  },

  onDescriptionChanged(newDescription) {

    var json = JSON.stringify({
      description: newDescription
    });

    $.ajax({
      url: "/api/roles/" + this.props.id + "/description",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {

        var role = this.state.role;
        role.description = newDescription;

        this.setState({ role: role });

      }.bind(this)
    });

  },

  showPermissionsDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  render() {

    var role = this.state.role;
    var self = this;

    if (role == null)
      return (<h1>Unknown role {this.props.id}</h1>);

    return (
      <div className="well">

        <h1><InlineEditor initialValue={role.name} onChange={this.onNameChanged} /></h1>
        <div><InlineEditor initialValue={role.description} onChange={this.onDescriptionChanged} /></div>

        <div className="page-header">
          <a href="#" onClick={this.showPermissionsDialog}>Change Permissions...</a>

          <PermissionSelector
            initialValue={role.permissions}
            url={"/api/roles/" + role.key + "/permissions/"}
            onChange={this.onPermissionsChanged}
            ref="dialog"
          />

        </div>


        <PermissionGrid
          permissions={role.permissions}
          navigate={this.props.navigate}
          url={"/api/roles/" + role.key + "/permissions/"}
        />

      </div>
    )
  }
});
