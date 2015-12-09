var PermissionOverview = React.createClass({

  render() {

    return (
      <Overview
        url="/api/permissions/all"
        deleteUrl="/api/permissions"
        tile={PermissionTile}
        create={CreatePermissionDialog}
      />
    );

  }

});
