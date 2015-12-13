var RoleGrid = React.createClass({

  render() {
    return (
      <Grid
        name="Roles"
        tile={RoleTile}
        selector={RoleSelector}
        {...this.props}
      />
    );
  }
})
