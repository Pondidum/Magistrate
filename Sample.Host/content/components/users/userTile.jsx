var UserTile = React.createClass({

  navigateToDetails(e) {
    e.preventDefault();
    this.props.navigate("singleuser", { key: this.props.content.key});
  },

  onDelete() {
    this.props.onRemove(this.props.content);
  },

  render() {

    var user = this.props.content;

    var confirmation = (
      <p>Are you sure you want to delete the user <strong>{user.name}</strong>?</p>
    );

    return (
      <Tile
        title={user.name}
        navigateTo={this.navigateToDetails}
        deleteUrl={"/api/users/" + user.key}
        onDelete={this.onDelete}
        dialogContent={confirmation}>
        <div>Roles: {user.roles.length}</div>
        <div>Includes: {user.includes.length}</div>
        <div>Revokes: {user.revokes.length}</div>
      </Tile>
    );
  }
});
