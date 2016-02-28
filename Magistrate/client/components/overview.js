import React from 'react'
import Grid from './grid'
import FilterBar from './filterbar'

var Overview = React.createClass({

  getInitialState() {
    return {
      filter: ""
    };
  },

  onFilterChanged(value) {
    this.setState({
      filter: value
    });
  },

  render() {

    var filter = new RegExp(this.state.filter, "i");

    var collection = this.props.collection
      .filter(function(item) {
        var isName =  item.name.search(filter) != -1;
        var isDescription = (item.description || "").search(filter) != -1;

        return isName || isDescription;
      });

    return (
      <div>
        <div>
          <div className="row">
            <div className="col-sm-2">
              <this.props.create onCreate={this.props.onAdd} url={this.props.url} />
            </div>
            <FilterBar filterChanged={this.onFilterChanged} />
          </div>
            <Grid
              name="???"
              tile={this.props.tile}
              {...this.props}
              collection={collection}
            />

        </div>
      </div>
    );

  }

});

export default Overview
