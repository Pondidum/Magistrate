var FilterBar = React.createClass({

  onChange(e) {
    this.props.filterChanged(e.target.value);
  },

  render() {

    return (
      <div className="row filter-bar">
        <div className="col-sm-12">
          <input type="text" className="form-control" placeholder="filter..." onChange={this.onChange} ref="filter" />
        </div>
      </div>
    );
  }

});
