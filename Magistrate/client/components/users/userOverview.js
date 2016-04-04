import React, { Component } from 'react'
import { connect } from 'react-redux'

import UserTile from './usertile'
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
    tile={UserTile}
    buttons={[ <CreateUserDialog /> ]}
  />
)

export default connect(mapStateToProps)(UserOverview);
