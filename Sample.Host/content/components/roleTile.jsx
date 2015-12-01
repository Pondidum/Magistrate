var RoleTile = React.createClass({

  navigateToDetails(e) {
    e.preventDefault();
  },

  onDelete() {
    this.props.onRemove(this.props.content);
  },

  render() {

    var role = this.props.content;

    var confirmation = (
      <p>Are you sure you want to delete the role <strong>{role.name}</strong>?</p>
    );

    return (
      <Tile
        title={role.name}
        navigateTo={this.navigateToDetails}
        deleteUrl={"/api/roles/" + role.key}
        onDelete={this.onDelete}
        dialogContent={confirmation}>
        <p>{role.description}</p>
      </Tile>
    );
  }

});
