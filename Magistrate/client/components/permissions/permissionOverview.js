import React, { Component } from 'react'
import { connect } from 'react-redux'

import Overview from '../Overview'
import PermissionPlate from './PermissionPlate'
import CreatePermissionDialog from './CreatePermissionDialog'

const mapStateToProps = (state) => {
  return {
    permissions: state.permissions
  }
}

const PermissionOverview = ({ permissions }) => (
  <Overview
    items={permissions}
    tile={PermissionPlate}
    buttons={[ <CreatePermissionDialog key={1} /> ]}
  />
)

export default connect(mapStateToProps)(PermissionOverview)
