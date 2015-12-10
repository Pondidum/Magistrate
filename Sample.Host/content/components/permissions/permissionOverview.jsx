var PermissionOverview = React.createClass({

  render() {

    return (
      <Overview
        url="/api/permissions"
        tile={PermissionTile}
        create={CreatePermissionDialog}
      />
    );

  }

});
