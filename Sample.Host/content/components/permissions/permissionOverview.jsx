var PermissionOverview = React.createClass({

  render() {

    return (
      <Overview
        tile={PermissionTile}
        create={CreatePermissionDialog}
        {...this.props}
      />
    );

  }

});
