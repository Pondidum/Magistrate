var RoleOverview = React.createClass({

  render() {

    return (
      <Overview
        url="/api/roles/all"
        deleteUrl="/api/roles"
        tile={RoleTile}
        create={CreateRoleDialog}
        navigate={this.props.navigate}
      />
    );

  }

});
