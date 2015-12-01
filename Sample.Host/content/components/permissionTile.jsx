var PermissionTile = React.createClass({

  navigateToDetails(e) {
    e.preventDefault();
  },

  onDelete() {
    this.props.onRemove(this.props.permission);
  },

  render() {

    var permission = this.props.permission;

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
      </Tile>
    );
  }

});
