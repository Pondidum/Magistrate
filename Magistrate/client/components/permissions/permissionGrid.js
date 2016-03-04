import React from 'react'
import Grid from '../grid'
import PermissionTile from './permissionTile'
import PermissionSelector from './permissionSelector'

var PermissionGrid = React.createClass({

  render() {
    return (
      <Grid
        name="permissions"
        tile={PermissionTile}
        selector={PermissionSelector}
        {...this.props}
      />
    );
  }
});

export default PermissionGrid
