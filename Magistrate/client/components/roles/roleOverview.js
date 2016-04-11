import React, { Component } from 'react'
import { connect } from 'react-redux'

import Overview from '../overview'
import RoleTile from './roleTile'
import CreateRoleDialog from './CreateRoleDialog'

const mapStateToProps = (state) => {
  return {
    roles: state.roles
  }
}

const RoleOverview = ({ roles }) => (
  <Overview
    items={roles}
    tile={RoleTile}
    buttons={[<CreateRoleDialog key={1} />]}
  />

)

export default connect(mapStateToProps)(RoleOverview)
