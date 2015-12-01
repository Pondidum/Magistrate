var RoleOverview = React.createClass({

  render() {

    return (
      <Overview
        url="/api/roles/all"
        tile={RoleTile}
        create={CreateRoleDialog}
      />
    );

  }

});
