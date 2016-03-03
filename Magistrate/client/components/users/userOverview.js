import React from 'react'
import { connect } from 'react-redux'

import UserTile from './usertile'
import CreateUserDialog from './CreateUserDialog'
import Overview from '../overview'

const SimpleOverview = ({ users }) => (
  <div>
    <div className="row">
      <div className="col-sm-2">
        <CreateUserDialog  />
      </div>
    </div>
    <div className="row">
      <ul className="list-unstyled list-inline col-sm-12">
        {users.map((user, i) => <UserTile key={i} content={user} /> )}
      </ul>
    </div>
  </div>
);


const mapStateToProps = (state) => {
  return {
    users: state.users
  }
}

const UserOverview = connect(mapStateToProps)(SimpleOverview);

export default UserOverview
