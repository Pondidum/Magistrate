var RoleTile = React.createClass({

  getInitialState() {
    return {
      role: null
    };
  },

  navigateToDetails(e) {
    e.preventDefault();
    this.props.navigate("singlerole", { key: this.props.content.key});
  },

  getRole() {
    return this.state.role || this.props.content;
  },

  editRoleAction(e) {
    e.preventDefault();

    var self = this;

    this.refs.editDialog.open(
      this.getRole(),
      (p) => { self.setState({ role: p }); }
    );

  },

  onDelete() {
    this.props.onRemove(this.props.content);
  },

  render() {

    var role = this.getRole();

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
        <EditRoleDialog ref="editDialog" />
      </Tile>
    );
  }

});
