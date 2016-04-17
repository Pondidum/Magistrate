import React, { Component } from 'react'
import { connect } from 'react-redux'

import RolePlate from './RolePlate'
import CreateRoleDialog from './CreateRoleDialog'
import Overview from '../Overview'

const mapStateToProps = (state) => {
  return {
    roles: state.roles
  }
}

const RoleOverview = ({ roles }) => (
  <Overview
    items={roles}
    tile={RolePlate}
    buttons={[ <CreateRoleDialog key={1} /> ]}
  />
)

export default connect(mapStateToProps)(RoleOverview)
