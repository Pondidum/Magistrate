import React, { Component } from 'react'
import { connect } from 'react-redux'

//import UserTile from './usertile'
import Plate from '../Plate'
import CreateUserDialog from './CreateUserDialog'
import Overview from '../overview'

const mapStateToProps = (state) => {
  return {
    users: state.users
  }
}

const UserPlate = ({ content }) => (
  <Plate title={content.name}>
    <span>Roles: {content.roles.length}</span>
    <span>Includes: {content.includes.length}</span>
    <span>Revokes: {content.revokes.length}</span>
  </Plate>
)

const UserOverview = ({ users }) => (
  <Overview
    items={users}
    tile={UserPlate}
    buttons={[ <CreateUserDialog /> ]}
  />
)

export default connect(mapStateToProps)(UserOverview);
