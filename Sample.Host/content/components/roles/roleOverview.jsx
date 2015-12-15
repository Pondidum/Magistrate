var RoleOverview = React.createClass({

  render() {

    return (
      <Overview
        tile={RoleTile}
        create={CreateRoleDialog}
        {...this.props}
      />
    );

  }

});
