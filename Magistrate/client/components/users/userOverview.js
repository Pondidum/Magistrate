import React from 'react'
import { Component } from 'react'

import { connect } from 'react-redux'

import UserTile from './usertile'
import CreateUserDialog from './CreateUserDialog'
import Overview from '../overview'
import FilterBar from '../filterBar'

//const SimpleOverview = ({ users }) => (
class SimpleOverview extends Component {

  constructor() {
    super();
    this.state = { filter: '' };
  }

  render() {
    return (
    <div>
      <div className="row">
        <div className="col-sm-2">
          <CreateUserDialog  />
        </div>
        <FilterBar filterChanged={value => this.setState({ filter: value })} />
      </div>
      <div className="row">
        <ul className="list-unstyled list-inline col-sm-12">
          {this.props.users
            .filter(user => {
              var exp = new RegExp(this.state.filter, "i");
              var isName =  user.name.search(exp) != -1;
              var isDescription = (user.description || "").search(exp) != -1;

              return isName || isDescription;
            })
            .map((user, i) => (
              <UserTile key={i} content={user} />
            ))
          }
        </ul>
      </div>
    </div>
    )
  }
}

const mapStateToProps = (state) => {
  return {
    users: state.users
  }
}

const UserOverview = connect(mapStateToProps)(SimpleOverview);

export default UserOverview
