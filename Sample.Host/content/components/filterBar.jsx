var FilterBar = React.createClass({

  onChange(e) {
    this.props.filterChanged(e.target.value);
  },

  render() {

    return (
      <div className={this.props.className}>
        <input type="text" className="form-control" placeholder="filter..." onChange={this.onChange} ref="filter" />
      </div>
    );
  }

});
