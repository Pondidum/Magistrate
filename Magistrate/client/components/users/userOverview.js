import React from 'react'
import { connect } from 'react-redux'

import UserTile from './usertile'
import CreateUserDialog from './CreateUserDialog'
import Overview from '../overview'


const SimpleGrid = ({ children }) => (
  <div>
    <div className="row">
      <ul className="list-unstyled list-inline col-sm-12">
        {children}
      </ul>
    </div>
  </div>
);

const SimpleOverview = ({ users }) => (
  <div>
    <div>
      <div className="row">
        <div className="col-sm-2">
          <CreateUserDialog  />
        </div>
      </div>
      <SimpleGrid>
        {users.map((user, i) => <UserTile key={i} content={user} /> )}
      </SimpleGrid>
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

// var UserOverview = React.createClass({
//
//   render() {
//
//     return (
//       <Overview
//         tile={UserTile}
//         create={CreateUserDialog}
//         collection={this.props.collection}
//         {...this.props}
//       />
//     );
//
//   }
// });
//
// export default UserOverview
