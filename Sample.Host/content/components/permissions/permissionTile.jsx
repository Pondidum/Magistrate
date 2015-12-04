var PermissionTile = React.createClass({

  getInitialState() {
    return {
      permission: null
    };
  },

  getPermission() {
    return this.state.permission || this.props.content;
  },

  navigateToDetails(e) {
    e.preventDefault();
    this.refs.editDialog.open(this.getPermission());
  },

  onEdit(permission) {
    this.setState({
      permission: permission
    });
  },

  onDelete() {
    this.props.onRemove(this.props.content);
  },

  render() {

    var permission = this.getPermission();

    var confirmation = (
      <p>Are you sure you want to delete the permission <strong>{permission.name}</strong>?</p>
    );

    return (
      <Tile
        title={permission.name}
        navigateTo={this.navigateToDetails}
        deleteUrl={"/api/permissions/" + permission.key}
        onDelete={this.onDelete}
        dialogContent={confirmation}>
        <p>{permission.description}</p>
        <EditPermissionDialog onEdit={this.onEdit} ref="editDialog" />
      </Tile>
    );
  }

});
