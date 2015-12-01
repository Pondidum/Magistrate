var PermissionOverview = React.createClass({

  render() {

    return (
      <Overview
        url="/api/permissions/all"
        tile={PermissionTile}
        create={CreatePermissionDialog}
      />
    );

  }

});
