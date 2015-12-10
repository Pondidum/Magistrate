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

  onDelete(success, error) {

    var self = this;
    var role = this.getRole();

    $.ajax({
      url: this.props.url,
      method: 'DELETE',
      cache: false,
      data: JSON.stringify([ role.key ]),
      success: function() {
        success();
        self.props.onRemove(role);
      },
      error: error
    });

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
        onDelete={this.onDelete}
        dialogContent={confirmation}>
        <p>{role.description}</p>
        <EditRoleDialog ref="editDialog" />
      </Tile>
    );
  }

});
