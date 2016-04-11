import React, { Component } from 'react'
import FilterBar from './filterbar'
import Grid from './Grid'

class Overview extends Component {

  constructor() {
    super();
    this.state = { filter: "" };
  }

  render() {

    const exp = new RegExp(this.state.filter, "i");
    const filter = item => {
      var isName =  item.name.search(exp) != -1;
      var isDescription = (item.description || "").search(exp) != -1;

      return isName || isDescription;
    }

    return (
      <div>
        <div className="row">
          <div className="col-sm-2">
            {this.props.buttons}
          </div>
          <FilterBar filterChanged={value => this.setState({ filter: value })} />
        </div>
        <Grid collection={this.props.items} filter={filter} tile={this.props.tile} />
      </div>
    )
  }
}

export default Overview
