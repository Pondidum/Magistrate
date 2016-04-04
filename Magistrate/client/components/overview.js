import React, { Component } from 'react'
import FilterBar from './filterbar'

class Overview extends Component {

  constructor() {
    super();
    this.state = { filter: "" };
  }

  render() {

    var exp = new RegExp(this.state.filter, "i");
    var tiles = this.props.items
      .filter(item => {
        var isName =  item.name.search(exp) != -1;
        var isDescription = (item.description || "").search(exp) != -1;

        return isName || isDescription;
      })
      .map((item, i) => (
        <this.props.tile key={i} content={item} />
      ));

    return (
      <div>
        <div className="row">
          <div className="col-sm-2">
            {this.props.buttons}
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

export default Overview
