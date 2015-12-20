var SingleRoleView = React.createClass({

  onNameChanged(newName) {

    var json = JSON.stringify({
      name: newName
    });

    $.ajax({
      url: this.props.url + "/name",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {

        var role = this.props.role;
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
      url: this.props.url + "/description",
      cache: false,
      method: "PUT",
      data: json,
      success: function() {

        var role = this.props.role;
        role.description = newDescription;

        this.setState({ role: role });

      }.bind(this)
    });

  },

  onAddPermission(permission) {
    var role = this.props.role;
    role.permissions = collection.add(role.permissions, permission);

    this.setState({ role: role });
  },

  onRemovePermission(permission) {
    var role = this.props.role;
    role.permissions = collection.remove(role.permissions, permission);

    this.setState({ role: role });
  },

  onChangePermissions(added, removed) {
    var role = this.props.role;
    role.permissions = collection.change(role.permissions, added, removed);

    this.setState({ role: role });
  },

  render() {

    var role = this.props.role;
    var self = this;

    if (role == null)
      return (<h1>Unknown role {this.props.roleKey}</h1>);

    return (
      <div className="well">

        <h1><InlineEditor initialValue={role.name} onChange={this.onNameChanged} /></h1>
        <div><InlineEditor initialValue={role.description} onChange={this.onDescriptionChanged} /></div>

        <PermissionGrid
          collection={role.permissions}
          onAdd={this.onAddPermission}
          onRemove={this.onRemovePermission}
          onChange={this.onChangePermissions}
          navigate={this.props.navigate}
          url={this.props.url + "/permissions/"}
          tileSize={self.props.tileSize}
        />

      </div>
    )
  }

});
