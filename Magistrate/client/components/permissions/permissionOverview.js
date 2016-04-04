import React, { Component } from 'react'
import { connect } from 'react-redux'

import Overview from '../overview'
import PermissionTile from './permissionTile'
import CreatePermissionDialog from './createPermissionDialog'

const mapStateToProps = (state) => {
  return {
    permissions: state.permissions
  }
}

const PermissionOverview = ({ permissions }) => (
  <Overview
    items={permissions}
    tile={PermissionTile}
    buttons={[ <CreatePermissionDialog /> ]}
  />
)

export default connect(mapStateToProps)(PermissionOverview)
