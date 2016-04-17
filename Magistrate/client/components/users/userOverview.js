import React, { Component } from 'react'
import { connect } from 'react-redux'

import UserPlate from './UserPlate'
import CreateUserDialog from './CreateUserDialog'
import Overview from '../overview'

const mapStateToProps = (state) => {
  return {
    users: state.users
  }
}

const UserOverview = ({ users }) => (
  <Overview
    items={users}
    tile={UserPlate}
    buttons={[ <CreateUserDialog key={1} /> ]}
  />
)

export default connect(mapStateToProps)(UserOverview);
