var PermissionTile = React.createClass({

  getInitialState() {
    return {
      permission: null
    };
  },

  getPermission() {
    return this.state.permission || this.props.content;
  },

  editPermissionAction(e) {
    e.preventDefault();

    var self = this;

    this.refs.editDialog.open(
      this.getPermission(),
      (p) => { self.setState({ permission: p }); }
    );

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
        deleteUrl={"/api/permissions/" + permission.key}
        onDelete={this.onDelete}
        editAction={this.editPermissionAction}
        dialogContent={confirmation}>
        <p>{permission.description}</p>
        <EditPermissionDialog ref="editDialog" />
      </Tile>
    );
  }

});
