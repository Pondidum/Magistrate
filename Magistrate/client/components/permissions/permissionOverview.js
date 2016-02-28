import React from 'react'

var PermissionOverview = React.createClass({

  render() {

    return (
      <Overview
        tile={PermissionTile}
        create={CreatePermissionDialog}
        collection={this.props.collection}
        {...this.props}
      />
    );

  }

});
