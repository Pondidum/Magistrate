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

  onDelete(success, error) {

    var self = this;
    var permission = this.getPermission();

    $.ajax({
      url: this.props.deleteUrl,
      method: 'DELETE',
      cache: false,
      data: JSON.stringify([ permission.key ]),
      success: function() {
        success();
        self.props.onRemove(permission);
      },
      error: error
    });

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
        onDelete={this.onDelete}
        editAction={editAction}
        dialogContent={confirmation}>
        <p>{permission.description}</p>
        <EditPermissionDialog ref="editDialog" />
      </Tile>
    );
  }

});
