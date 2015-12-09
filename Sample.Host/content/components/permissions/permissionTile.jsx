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

    var editAction = this.props.showEdit ? this.editPermissionAction : null;

    return (
      <Tile
        title={permission.name}
        deleteUrl={this.props.deleteUrl}
        onDelete={this.onDelete}
        editAction={editAction}
        dialogContent={confirmation}>
        <p>{permission.description}</p>
        <EditPermissionDialog ref="editDialog" />
      </Tile>
    );
  }

});
