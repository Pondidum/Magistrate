import React from 'react'
import { Component } from 'react'

import { connect } from 'react-redux'

import Overview from '../overview'
import PermissionTile from './permissionTile'
import CreatePermissionDialog from './createPermissionDialog'
import FilterBar from '../filterbar'

const mapStateToProps = (state) => {
  return {
    permissions: state.permissions
  }
}

class PermissionOverview extends Component {

  constructor() {
    super();
    this.state = { filter: '' };
  }

  render() {

    var exp = new RegExp(this.state.filter, "i");
    var userTiles = this.props.permissions
      .filter(permission => {
        var isName =  permission.name.search(exp) != -1;
        var isDescription = (permission.description || "").search(exp) != -1;

        return isName || isDescription;
      })
      .map((permission, i) => (
        <PermissionTile key={i} content={permission} />
      ));

    return (
      <div>
        <div className="row">
          <div className="col-sm-2">
            <CreatePermissionDialog  />
          </div>
          <FilterBar filterChanged={value => this.setState({ filter: value })} />
        </div>
        <div className="row">
          <ul className="list-unstyled list-inline col-sm-12">
            {userTiles}
          </ul>
        </div>
      </div>
    )
  }
}

export default connect(mapStateToProps)(PermissionOverview)
