import React from 'react'
import Overview from '../overview'
import PermissionTile from './permissionTile'
import CreatePermissionDialog from './createPermissionDialog'

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

export default PermissionOverview
