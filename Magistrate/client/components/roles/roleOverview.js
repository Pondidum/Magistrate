import React from 'react'
import { Component } from 'react'

import { connect } from 'react-redux'

import Overview from '../overview'
import RoleTile from './roleTile'
import CreateRoleDialog from './CreateRoleDialog'
import FilterBar from '../filterbar'

class RoleOverview extends Component {

  constructor() {
    super();
    this.state = { filter: "" };
  }

  render() {

    var exp = new RegExp(this.state.filter, "i");
    var tiles = this.props.roles
      .filter(role => {
        var isName =  role.name.search(exp) != -1;
        var isDescription = (role.description || "").search(exp) != -1;

        return isName || isDescription;
      })
      .map((role, i) => (
        <RolteTile key={i} content={role} />
      ));

    return (
      <div>
        <div className="row">
          <div className="col-sm-2">
            <CreateRoleDialog  />
          </div>
          <FilterBar filterChanged={value => this.setState({ filter: value })} />
        </div>
        <div className="row">
          <ul className="list-unstyled list-inline col-sm-12">
            {tiles}
          </ul>
        </div>
      </div>
    )
  }
}

const mapStateToProps = (state) => {
  return {
    roles: state.roles
  }
}

export default connect(mapStateToProps)(RoleOverview)
