var RoleOverview = React.createClass({

  render() {

    return (
      <Overview
        url="/api/roles"
        tile={RoleTile}
        create={CreateRoleDialog}
        navigate={this.props.navigate}
      />
    );

  }

});
