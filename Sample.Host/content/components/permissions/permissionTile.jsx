var PermissionTile = React.createClass({

  getInitialState() {
    return {
      permission: null
    };
  },

  navigateToDetails(e) {
    e.preventDefault();
    this.props.navigate("singlepermission", { key: this.props.content.key});
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
      url: this.props.url,
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

    return (
      <Tile
        title={permission.name}
        navigateTo={this.navigateToDetails}
        onDelete={this.onDelete}
        dialogContent={confirmation}
        tileSize={this.props.tileSize}>
        {permission.description}
        <EditPermissionDialog url={this.props.url + "/" + permission.key} ref="editDialog" />
      </Tile>
    );
  }

});
