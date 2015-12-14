var PermissionOverview = React.createClass({

  render() {

    return (
      <Overview
        url={this.props.url}
        tile={PermissionTile}
        create={CreatePermissionDialog}
        navigate={this.props.navigate}
      />
    );

  }

});
